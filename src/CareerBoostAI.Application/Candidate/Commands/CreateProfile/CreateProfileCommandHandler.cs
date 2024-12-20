using CareerBoostAI.Application.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Application.Services.ReadService.CandidateReadService;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.Factories;
using CareerBoostAI.Domain.Repositories;
using CareerBoostAI.Domain.ValueObjects;
using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
{

    private readonly ICandidateReadService _candidateReadService;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly ICvParseService _cvParseService;
    private readonly IEmailSender _emailSender;
    private readonly ICandidateFactory _candidateFactory;
    private readonly ICandidateCvFactory _candidateCvFactory;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProfileCommandHandler(
        IFileStorageService fileStorageService, 
        ICvParseService cvParseService, IMediator mediator, 
        IEmailSender emailSender, ICandidateRepository candidateRepository, 
        ICandidateReadService candidateReadService, ICandidateCvFactory candidateCvFactory, 
        ICandidateFactory candidateFactory, IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _cvParseService = cvParseService;
        _emailSender = emailSender;
        _candidateRepository = candidateRepository;
        _candidateReadService = candidateReadService;
        _candidateCvFactory = candidateCvFactory;
        _candidateFactory = candidateFactory;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var candidateDto = await _candidateReadService.SearchCandidateByEmailAsync(request.Email, cancellationToken);
        if (candidateDto != null)
        {
            throw new DuplicateCandidateProfileException(request.Email);
        }
        
        Domain.Entities.Candidate candidate = _candidateFactory.Create(
            CandidateId.New(),
            CandidateFirstName.Create(request.FirstName),
            CandidateLastName.Create(request.LastName),
            new List<CandidateEmail>() { CandidateEmail.Create(request.Email) },
            CandidateDOB.Create(request.DateOfBirth),
            new List<PhoneNumber>() { PhoneNumber.Create(request.PhoneCode, request.PhoneNumber) });
        
        // validate extension before save
        if (!CvFile.IsSupportedFileType(request.CvFileName))
        {
            throw new UnsupportedFileTypeException(request.CvFileName);
        }
        var cvFilePath = await _fileStorageService.UploadFileAsync(request.CvFile, request.CvFileName, cancellationToken);
        var cvFile = CvFile.Create(request.CvFileName, CvStorageMedium.AzureStorageBlob, cvFilePath);
        
        var parsedCv = await _cvParseService.ParseCvAsync(request.CvFile, request.CvFileName, cancellationToken);
        
        var candidateCv = _candidateCvFactory.Create(CandidateCvId.New(), cvFile);
        candidate.AddCv(candidateCv);
        
        var adminNotificationMessage =
            $"A new candidate profile has been created for {request.FirstName} {request.LastName}.";
        await _emailSender.SendEmailToAdminAsync(subject: "New Candidate Profile Created", body: adminNotificationMessage);
        await _candidateRepository.AddCandidateAsync(candidate);
        await _unitOfWork.SaveChangesAsync(cancellationToken); 
    }
}
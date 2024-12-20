using CareerBoostAI.Application.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Application.Services.ReadService.CandidateReadService;
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
            
        }
        var candidateID = new CandidateId(Guid.NewGuid());
        var candidate = _candidateFactory.Create(
            candidateID,
            new CandidateFirstName(request.FirstName),
            new CandidateLastName(request.LastName),
            new List<CandidateEmail>() { new CandidateEmail(request.Email) },
            new CandidateDOB(request.DateOfBirth),
            new List<PhoneNumber>() { new PhoneNumber(request.PhoneNumber) });
        
        var cvFilePath = await _fileStorageService.UploadFileAsync(request.CvFile, request.CvFileName, cancellationToken);
        var parsedCv = await _cvParseService.ParseCvAsync(request.CvFile, request.CvFileName, cancellationToken);
        var candidateCvId = new CandidateCvId(Guid.NewGuid());
        var candidateCv = _candidateCvFactory.Create();
        var adminNotificationMessage =
            $"A new candidate profile has been created for {request.FirstName} {request.LastName}.";
        await _emailSender.SendEmailToAdminAsync(subject: "New Candidate Profile Created", body: adminNotificationMessage);
        await _candidateRepository.AddCandidateAsync(candidate);
        await _candidateRepository.AddCandidateCvAsync(candidateCv);
        await _unitOfWork.SaveChangesAsync(cancellationToken); 
    }
}
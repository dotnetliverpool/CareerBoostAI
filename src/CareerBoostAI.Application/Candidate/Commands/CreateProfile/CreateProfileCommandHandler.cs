using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
{

    private readonly ICandidateReadService _candidateReadService;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IEmailSender _emailSender;
    private readonly ICandidateFactory _candidateFactory;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProfileCommandHandler(
        IFileStorageService fileStorageService, 
        ICvParseService cvParseService, IMediator mediator, 
        IEmailSender emailSender, ICandidateRepository candidateRepository, 
        ICandidateReadService candidateReadService, ICandidateCvFactory candidateCvFactory, 
        ICandidateFactory candidateFactory, IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _emailSender = emailSender;
        _candidateRepository = candidateRepository;
        _candidateReadService = candidateReadService;
        _candidateFactory = candidateFactory;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        
        Domain.Candidate.Candidate candidate = _candidateFactory.Create(
            FirstName.Create(request.FirstName),
            LastName.Create(request.LastName),
            DateOfBirth.Create(request.DateOfBirth),
            Email.Create(request.Email),
            PhoneNumber.Create(request.PhoneCode, request.PhoneNumber));
       
        var storageAddress = await _fileStorageService.UploadFileAsync(
            request.CvFile, request.CvFileName, cancellationToken);
        var cvFile = CvFile.Create(request.CvFileName, 
            _fileStorageService.GetMedium(), storageAddress);
        candidate.RegisterCv(cvFile);
        
        
        var adminNotificationMessage =
            $"A new candidate profile has been created for {request.FirstName} {request.LastName}.";
        await _emailSender.SendEmailToAdminAsync(subject: "New Candidate Profile Created", body: adminNotificationMessage);
        
        await _candidateRepository.AddAsync(candidate.AsDto());
        await _unitOfWork.SaveChangesAsync(cancellationToken); 
    }

    private async Task Validate(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var isRegistered = await _candidateReadService.CandidateExistsByEmailAsync(request.Email, cancellationToken);
        if (isRegistered)
        {
            throw new DuplicateCandidateProfileException(request.Email);
        }
    }
}
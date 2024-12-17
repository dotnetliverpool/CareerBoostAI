using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand>
{
    public Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // create and validate Value objects
        
        // create and save entity
        
        // process cv -
        
            // ocr
            
            // AI model
            
            // notify admin
            
        // return response
    }
}
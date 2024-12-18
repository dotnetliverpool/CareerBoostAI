using CareerBoostAI.Application.Abstractions.Mediator;
using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
{
    public Task<object> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
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
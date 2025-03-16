# CareerBoostAI

[![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/download)
[![Azure](https://img.shields.io/badge/Azure-0078D4?style=for-the-badge&logo=microsoftazure&logoColor=white)](https://azure.microsoft.com/)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](https://www.gnu.org/licenses/gpl-3.0)
[!Tests](https://github.com/dotnetliverpool/CareerBoostAI/actions/workflows/test.yml/badge.svg)
[![DotNet Liverpool](https://img.shields.io/badge/DotNet_Liverpool-Community-blue?style=for-the-badge)](https://dotnetliverpool.org.uk)

[![CareerBoostAI Architecture](https://github.com/user-attachments/assets/b02351e0-b103-414d-bb93-33d32261cc1b)](https://miro.com/app/board/uXjVL3G3HTE=/?share_link_id=970213690232)

CareerBoostAI is an intelligent career development platform that helps professionals enhance their career growth through AI-powered insights and guidance. This project aims to offer AI assistance to give feedback on a candidate's CV and connect employers with candidates through our matching algorithm.

## �� Features
- **AI-powered CV analysis and recommendation**: Automatically evaluates CVs and provides improvement recommendations.
- **Candidate CV collation**: Aggregates candidate CVs for easier job matching.
- **Job collation from potential employers**: Collects job opportunities from employers to match with candidates.
- **AI-powered job-candidate compatibility analysis**: Uses AI to assess the compatibility between candidates and job roles, providing insights into the best fits.
- **Career path analysis and recommendations**: Provides personalised recommendations for career progression.
- **Skill gap assessment**: Identifies skill gaps and suggests relevant learning paths.
- **Personalized learning recommendations**: Recommends learning resources based on career goals and skill requirements.
- **Professional development tracking**: Tracks progress in skill development and professional milestones.

## 🏗️ Architecture

The project follows **Clean Architecture** principles, with a **Domain-Driven Design** approach and utilizes **CQRS** (Command Query Responsibility Segregation) for efficient handling of commands and queries. The codebase is structured as follows:

```
src/
├── CareerBoostAI.Api/           # API endpoints and configuration
├── CareerBoostAI.Application/   # Application business logic and use cases
├── CareerBoostAI.Domain/        # Domain entities and business rules
├── CareerBoostAI.Infrastructure/ # External services, Optimised Domain Reporting (Queries) and data access
├── CareerBoostAI.Contracts/     # Shared DTOs and interfaces
└── CareerBoostAI.Shared.Abstractions/  # Shared abstractions and interfaces
```

## 🛠️ Technology Stack

- **.NET 8**: Core framework
- **Entity Framework Core**: Data access and ORM
- **Azure Functions**: Serverless compute
- **MySQL Database**: Data storage
- **Microsoft Semantic Kernel**: AI/ML Integration
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework

## 🏁 Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or JetBrains Rider
- MySql Database

### Local Development Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/CareerBoostAI.git
   cd CareerBoostAI
   ```

2. Configure local settings:
   - Copy `src/CareerBoostAI.Api/example.settings.json` to `local.settings.json`
   - Update the connection strings and API keys as needed

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   cd src/CareerBoostAI.Api
   dotnet run
   ```

The API will be available at `https://localhost:5001`

### Database Setup

1. Update the database:
   ```bash
   dotnet ef database update --startup-project .\src\CareerBoostAI.Api\CareerBoostAI.Api.csproj --project .\src\CareerBoostAI.Infrastructure\CareerBoostAI.Infrastructure.csproj --context CareerBoostReadDbContext
   ```

## 🧪 Running Tests

```bash
dotnet test
```

## 📝 Contributing

Please read [Contributing.md](Contributing.md) for details on our code of conduct and the process for submitting pull requests.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Support

For support, please open an issue in the GitHub repository or contact the maintainers.

## ✨ Acknowledgments

- Thanks to all contributors who have helped shape CareerBoostAI
- Special thanks to the .NET and Azure communities for their excellent tools and documentation

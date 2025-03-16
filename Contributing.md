# Welcome to CareerBoostAI

Thank you for considering contributing to **CareerBoostAI**! We're eager for contributions and thrilled that you've found your way here. 🚀 Whether you're fixing bugs, adding features, improving documentation, or suggesting enhancements, your help is invaluable.

## Table of Contents
- [Important Resources](#important-resources)
- [Getting Started](#getting-started)
- [Setting Up Your Development Environment](#setting-up-your-development-environment)
- [Testing](#testing)
- [How to Contribute](#how-to-contribute)
    - [Branching Strategy](#branching-strategy)
    - [Pull Request Guidelines](#pull-request-guidelines)
- [How to Report a Bug](#how-to-report-a-bug)
- [First Bugs for Contributors](#first-bugs-for-contributors)
- [How to Request an Enhancement](#how-to-request-an-enhancement)
- [Code Style Guide](#code-style-guide)
- [Code of Conduct](#code-of-conduct)
- [Who is Involved?](#who-is-involved)
- [Where Can I Ask for Help?](#where-can-i-ask-for-help)
- [Recognition and Appreciation](#recognition-and-appreciation)

## Important Resources
- 📜 **Docs & Handbook**: [CareerBoostAI Documentation](#) *(Add link when available)*
- 🛠 **Roadmap**: [Project Roadmap](#) *(Add link when available)*
- 🐛 **Issue Tracker**: [GitHub Issues](https://github.com/dotnetliverpool/CareerBoostAI/issues)
- 🏗 **Discussions & Community**: [GitHub Discussions](https://github.com/dotnetliverpool/CareerBoostAI/discussions)

## Getting Started
Before contributing, please:
1. Read this guide to understand how to contribute.
2. Check the [Issues Page](https://github.com/dotnetliverpool/CareerBoostAI/issues) to ensure your work isn't duplicating existing efforts.
3. Familiarise yourself with the [Roadmap](#) to align with project goals.
4. Be kind and respectful in all interactions.

## Setting Up Your Development Environment
For installation and setup, follow the [Setup Guide in README.md](README.md#setup).

## Testing
- **Unit tests** must be written for all **domain and application project logic**.
- Run tests before submitting a pull request:
  ```bash
  dotnet test
  ```

## How to Contribute

### Branching Strategy
- **Main Branch (`main`)**: Stable production-ready code.
- **Development Branch (`develop`)**: Base branch for all new work.
- **Feature Branches (`feature/your-feature-name`)**: For new features.
- **Bug Fix Branches (`bugfix/your-bug-name`)**: For bug fixes.

**Example workflow:**
```bash
git checkout develop
git pull origin develop
git checkout -b feature/new-feature
# Make changes and commit
git push origin feature/new-feature
```

## Pull Request Guidelines
- Keep PRs focused and avoid bundling multiple changes.
- Ensure your code follows the [Style Guide](#code-style-guide).
- Reference related issues using `Closes #issue_number`.
- Ensure all tests pass before submitting.
- Request a review from a maintainer.

## How to Report a Bug
- Go to the [Issues Page](https://github.com/dotnetliverpool/CareerBoostAI/issues).
- Check if the issue already exists.
- If not, create a new issue using the **Bug Report Template**.
- Include steps to reproduce, expected behaviour, and logs/screenshots if applicable.

## First Bugs for Contributors
New contributors can start with issues labeled **"good first issue"**. Find them [here](https://github.com/dotnetliverpool/CareerBoostAI/issues?q=is%3Aopen+label%3A"good+first+issue").

## How to Request an Enhancement
- Go to the [Issues Page](https://github.com/dotnetliverpool/CareerBoostAI/issues).
- Create a new issue and select the **Enhancement** label.
- Clearly describe the improvement and its potential benefits.

## Code Style Guide
- Follow **.NET best practices** and **DDD (Domain-Driven Design) principles**.
- Maintain **CQRS (Command Query Responsibility Segregation) patterns**.
- Use meaningful variable and function names.
- Keep methods short and focused.
- Avoid nesting where possible in favour of guard clauses and early returns.
- Format your code using:
  ```bash
  dotnet format
  ```

  ## Code of Conduct
We expect all contributors to be respectful and follow the [Code of Conduct](CODE_OF_CONDUCT.md).

## Who is Involved?
- **Project Leads**: [Samson Nwizugbe](https://github.com.nwizugbesamson), [Joshua Duxbury](https://github.com/p0onage)
- **Core Contributors**: [View Contributors](https://github.com/dotnetliverpool/CareerBoostAI/graphs/contributors)

## Where Can I Ask for Help?
- GitHub Discussions: [CareerBoostAI Discussions](https://github.com/dotnetliverpool/CareerBoostAI/discussions)
- Open an issue and tag it with **"question"**.

## Recognition and Appreciation
🌟 **Every contribution matters!** 🌟  
Contributors are recognised in the **GitHub Contributors Page** and major contributions will be acknowledged in releases.  

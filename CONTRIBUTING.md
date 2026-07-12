# CONTRIBUTING.md

## How to Contribute

We welcome contributions to the Grand Hotel ERP System! Please follow these guidelines to ensure a smooth collaboration.

## Code Standards

### Language
- **All code comments and documentation must be in English**
- Variable names, class names, and method names must be in English
- No non-English comments in production code

### C# Conventions
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use PascalCase for class and method names
- Use camelCase for local variables and parameters
- Use UPPER_SNAKE_CASE for constants

### Example:
```csharp
// ✅ GOOD
public class BillingService
{
    private const int MAX_RETRIES = 3;
    
    public void ProcessInvoice(Invoice invoice)
    {
        // Configure invoice processing
        var processor = new InvoiceProcessor();
    }
}

// ❌ BAD
public class BillingService
{
    // ගිණුම් සැකසුම
    private const int maxRetries = 3;
    
    public void ProcessInvoice(Invoice invoice)
    {
        var p = new InvoiceProcessor();
    }
}
```

## Git Workflow

### 1. Create a Feature Branch
```bash
git checkout main
git pull origin main
git checkout -b feature/your-feature-name
```

Use branch naming conventions:
- `feature/` - New features
- `fix/` - Bug fixes
- `docs/` - Documentation updates
- `refactor/` - Code refactoring
- `test/` - Test improvements

### 2. Commit Messages
Use clear, descriptive commit messages following this format:

```
<type>: <subject>

<body (optional)>
```

Types:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation
- `style:` - Code style changes (formatting, etc.)
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Build, dependencies, tooling

Examples:
```bash
git commit -m "feat: add room filtering by amenities"
git commit -m "fix: resolve null reference exception in BillingController"
git commit -m "docs: update README with setup instructions"
```

### 3. Push and Create Pull Request
```bash
git push origin feature/your-feature-name
```

Then create a Pull Request on GitHub with:
- Clear title describing the change
- Description of what was changed and why
- Link to any related issues
- Screenshots (if UI changes)

## Testing

Before submitting a pull request:

```bash
# Run tests
dotnet test

# Check code builds without warnings
dotnet build
```

## Database Migrations

When modifying models:

```bash
# Create a new migration
dotnet ef migrations add DescriptiveNameOfChange

# Review the generated migration file in Migrations/
# Then apply it
dotnet ef database update
```

**Important:** Always review generated migrations before committing.

## Secrets and Configuration

### DO NOT:
- ❌ Commit passwords, API keys, or connection strings with credentials
- ❌ Commit `.env` files or `secrets.json`
- ❌ Hardcode configuration values

### DO:
- ✅ Use `appsettings.json` for non-sensitive configuration
- ✅ Use User Secrets for development: `dotnet user-secrets set "key" "value"`
- ✅ Use environment variables in production
- ✅ Document required secrets in README or `.env.example`

### Setting Up User Secrets

```bash
# Initialize user secrets for the project (one time)
dotnet user-secrets init

# Set a secret
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "key"
```

## Pull Request Review Process

1. Code review by maintainers
2. All tests must pass
3. No merge conflicts
4. Follow project conventions
5. Approval before merge

## Questions?

- Open an issue for discussions
- Check existing issues first
- Use clear, descriptive titles
- Include code examples or screenshots where relevant

Thank you for contributing!

using FluentValidation;
using FluentValidator;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.domain.validators;
internal class TransactionValidator : AbstractValidator<TransactionEntry>
{
    private static readonly List<string> TransactionStatus = new() { "Aprovada", "Reprovada" };
    public TransactionValidator()
    {
        RuleFor(t => t.Amount).GreaterThan(0);
        RuleFor(t => t.TransactionDateTime).LessThan(DateTime.UtcNow);
        //inserir aqui a regra para o Status
    }
}

using FluentValidation;
using FluentValidator;
using TransactionFilter.domain.transaction;

namespace TransactionFilter.domain.validators;
internal class TransactionValidator : AbstractValidator<Transaction>
{
    private static readonly List<string> TransactionStatus = new() { "Aprovada", "Reprovada" };
    public TransactionValidator()
    {
        RuleFor(t => t.Document).NotNull().NotEmpty();
        RuleFor(t => t.CardNumber).NotNull().NotEmpty();
        RuleFor(t => t.Amount).GreaterThan(0);
        RuleFor(t => t.TransactionDateTime).LessThan(DateTime.Now);
        //inserir aqui a regra para o Status
    }
}

namespace gain_api.Validators.Auditoria
{
    using core.Dto;
    using FluentValidation;

    public class SetResponsableAuditoriaDtoValidator : AbstractValidator<SetResponsableAuditoriaDto>
    {
        public SetResponsableAuditoriaDtoValidator()
        {
            RuleFor(x => x.AuditoriaId).GreaterThan(0);
            RuleFor(x => x.ResponsableId).GreaterThan(0);
        }
    }
}

namespace gain_api.Validators.Hallazgo
{
    using core.Dto;
    using FluentValidation;

    public class SetHallazgoAuditoriaDtoValidator : AbstractValidator<SetHallazgoAuditoriaDto>
    {
        public SetHallazgoAuditoriaDtoValidator()
        {
            RuleFor(x => x.AuditoriaId).GreaterThan(0);
            RuleFor(x => x.HallazgoId).GreaterThan(0);
        }
    }
    
}

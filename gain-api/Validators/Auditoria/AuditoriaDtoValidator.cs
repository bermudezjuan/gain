namespace gain_api.Validators.Auditoria
{
    using core.Dto;
    using FluentValidation;
    using Helpers;

    public class AuditoriaDtoValidator : AbstractValidator<AuditoriaDto>
    {
        public AuditoriaDtoValidator()
        {
            RuleFor(a => a.Titulo).Must(ValidatorsHelper.ValidateString).WithMessage("Solo puede ingresar letras")
                .NotNull().NotEmpty().WithMessage("El campo Titulo es requerido.");
            RuleFor(a => a.FechaInicio)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("El campo Fecha Inicio debe ser mayor o igual a la fecha actual.")
                .NotEmpty().WithMessage("El campo Fecha Inicio es requerido.");
            RuleFor(a => a.FechaFin)
                .GreaterThan(DateTime.Now).WithMessage("El campo Fecha Inicio debe ser mayor a la fecha actual.")
                .NotEmpty().WithMessage("El campo Fecha Fin es requerido.");
            RuleFor(a => a.Area).NotNull().NotEmpty().WithMessage("El campo Area es requerido.");
            RuleFor(a => a.Estado).NotEmpty().WithMessage("El campo Estado es requerido.");
        }
    }
}

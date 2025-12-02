namespace gain_api.Validators.Hallazgo
{
    using core.Dto;
    using FluentValidation;
    
    public class HallazgoDtoValidator : AbstractValidator<HallazgoDto>
    {
        public HallazgoDtoValidator()
        {
            RuleFor(a => a.Descripcion)
                .NotNull().NotEmpty().WithMessage("El campo descripcion es requerido.");
            RuleFor(a => a.Tipo)
                .NotNull().NotEmpty().WithMessage("El campo Tipo es requerido.");
            RuleFor(a => a.Severidad)
                .NotNull().NotEmpty().WithMessage("El campo Severidad es requerido.");
            RuleFor(a => a.FechaDeteccion)
                .NotNull().NotEmpty().WithMessage("El campo Fecha Detección es requerido.");
        }
    }
}

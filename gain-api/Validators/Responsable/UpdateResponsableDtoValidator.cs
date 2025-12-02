namespace gain_api.Validators.Responsable
{
    using core.Dto;
    using FluentValidation;
    using Helpers;

    public class UpdateResponsableDtoValidator : AbstractValidator<UpdateResponsableDto>
    {
        public UpdateResponsableDtoValidator()
        {
            RuleFor(a => a.Nombre).Must(ValidatorsHelper.ValidateString).WithMessage("Solo pueden ingresar letras")
                .NotNull().NotEmpty().WithMessage("El campo Nombre es requerido.");
            RuleFor(a => a.Correo).Must(ValidatorsHelper.BeValidEmail).WithMessage("Debe proporcionar un correo válido.")
                .NotNull().NotEmpty().WithMessage("El campo Correo es requerido.");
            RuleFor(a => a.Area).Must(ValidatorsHelper.ValidateString).WithMessage("Solo pueden ingresar letras")
                .NotNull().NotEmpty().WithMessage("El campo Area es requerido.");
        }
    }
}

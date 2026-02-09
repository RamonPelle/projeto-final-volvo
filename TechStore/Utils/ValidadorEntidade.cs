using System.ComponentModel.DataAnnotations;

namespace TechStore.Utils
{
    public static class ValidadorEntidade
    {
        public static List<ValidationResult> Validar(object entidade)
        {
            var erros = new List<ValidationResult>();
            Validator.TryValidateObject(entidade, new ValidationContext(entidade), erros, validateAllProperties: true);

            return erros;
        }
    }
}

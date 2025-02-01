namespace HydroMeasure.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.UtcNow;
        }
    }
}
namespace HydroMeasure.Hibrid.Shared.Model.Alerta
{
    public class AlertaDto
    {
        public Guid Id { get; set; }
        public Guid UnidadeId { get; set; }
        public Guid CondominioId { get; set; }
        public string IdentificacaoUnidade { get; set; }
        public string Tipo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataAlerta { get; set; }
        public bool Resolvido { get; set; }
        public Guid? UsuarioResolvidoId { get; set; }
        public string? NomeUsuarioResolvido { get; set; }

        public AlertaDto(Guid id, Guid unidadeId, Guid condominioId, string identificacaoUnidade, string tipo, string mensagem, DateTime dataAlerta, bool resolvido, Guid? usuarioResolvidoId, string? nomeUsuarioResolvido)
        {
            Id = id;
            UnidadeId = unidadeId;
            CondominioId = condominioId;
            IdentificacaoUnidade = identificacaoUnidade;
            Tipo = tipo;
            Mensagem = mensagem;
            DataAlerta = dataAlerta;
            Resolvido = resolvido;
            UsuarioResolvidoId = usuarioResolvidoId;
            NomeUsuarioResolvido = nomeUsuarioResolvido;
        }
    }
}

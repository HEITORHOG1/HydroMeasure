using HydroMeasure.Domain.Entities.Base;
using HydroMeasure.Domain.Enums;

namespace HydroMeasure.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public bool Ativo { get; set; }

        // Navigation property para RelatoriosGerados (1-N)
        public List<Relatorio> RelatoriosGerados { get; set; }

        // Navigation property para AlertasResolvidos (1-N)
        public List<Alerta> AlertasResolvidos { get; set; }

        // Nova propriedade de navegação para Condominio (N-1) - Usuário pertence a um Condominio
        public Guid CondominioId { get; set; } // Chave estrangeira para Condominio

        public Condominio Condominio { get; set; } // Propriedade de navegação

        public Usuario()
        {
        }

        public Usuario(string nome, string email, string senha, PerfilUsuario perfil)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Perfil = perfil;
            Ativo = true;
        }

        //Update

        public void Update(string nome, string email, string senha, PerfilUsuario perfil, bool ativo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Perfil = perfil;
            Ativo = ativo;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}
namespace LogiTrip.Model.Entity
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        // Propriedades adicionais
        public string CPF_CNPJ { get; set; }

        public string NomeUsuario { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime UltimoLogin { get; set; }
        public string UltimoLoginIP { get; set; }
        public int TentativasLoginFalhas { get; set; }
    }
}
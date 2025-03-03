namespace LogiTrip.Hybrid.Shared.Endpoints
{
    public static class APIAuthEndpoints
    {
        public const string Login = BaseEndPoint.BaseUrl + "/Auth/login";
        public const string Register = BaseEndPoint.BaseUrl + "/Auth/register";
        public const string RegisterAdminProprietario = BaseEndPoint.BaseUrl + "/Auth/register/adminproprietario";
        public const string RefreshToken = BaseEndPoint.BaseUrl + "/Auth/refresh-token";
        public const string RegisterFuncionario = BaseEndPoint.BaseUrl + "/Auth/register-funcionario";
        public const string GetUsersAdminProprietario = BaseEndPoint.BaseUrl + "/Auth/users/admin-proprietario";


        // Novos endpoints hipotéticos para edição:
        public const string GetUsuarioById = BaseEndPoint.BaseUrl + "/Auth/user/{0}";

        public const string UpdateUsuario = BaseEndPoint.BaseUrl + "/Auth/update-user";

        public const string GetEstabelecimentosByProprietario = BaseEndPoint.BaseUrl + "/estabelecimento/proprietario";
        public const string GetUsuariosByEstabelecimento = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/usuarios";
        public const string CreateEstabelecimento = BaseEndPoint.BaseUrl + "/Estabelecimento";
        public const string UpdateEstabelecimento = BaseEndPoint.BaseUrl + "/Estabelecimento/{0}";
        public const string GetEstabelecimentoById = BaseEndPoint.BaseUrl + "/Estabelecimento/{0}";

        public const string GetFornecedoresByEstabelecimento = BaseEndPoint.BaseUrl + "/Fornecedores/estabelecimentos/{0}";
        public const string CreateFornecedor = BaseEndPoint.BaseUrl + "/Fornecedores/estabelecimentos/{0}";
        public const string UpdateFornecedor = BaseEndPoint.BaseUrl + "/Fornecedores/{0}";
        public const string GetFornecedorById = BaseEndPoint.BaseUrl + "/Fornecedores/{0}";

        public const string GetCategoriasByEstabelecimento = BaseEndPoint.BaseUrl + "/Categorias/estabelecimentos/{0}/categorias";
        public const string CreateCategoria = BaseEndPoint.BaseUrl + "/Categorias/estabelecimentos/{0}/categorias";
        public const string UpdateCategoria = BaseEndPoint.BaseUrl + "/Categorias/estabelecimentos/{0}/categorias/{1}";
        public const string GetCategoriaById = BaseEndPoint.BaseUrl + "/Categorias/estabelecimentos/{0}/categorias/{1}";

        public const string GetProdutosByEstabelecimento = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/produtos";
        public const string CreateProduto = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/produtos";
        public const string UpdateProduto = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/produtos/{1}";
        public const string GetProdutoById = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/produtos/{1}";
        public const string DeleteProduto = BaseEndPoint.BaseUrl + "/estabelecimento/{0}/produtos/{1}";

        public const string GetOpcoesByProduto = BaseEndPoint.BaseUrl + "/OpcaoProduto/{0}/{1}";
        public const string CreateOpcaoProduto = BaseEndPoint.BaseUrl + "/OpcaoProduto/{0}/{1}";
        public const string GetOpcaoProdutoById = BaseEndPoint.BaseUrl + "/OpcaoProduto/{0}/{1}/{2}";
        public const string UpdateOpcaoProduto = BaseEndPoint.BaseUrl + "/OpcaoProduto/{0}/{1}/{2}";
        public const string DeleteOpcaoProduto = BaseEndPoint.BaseUrl + "/OpcaoProduto/{0}/{1}/{2}";
        public const string ReplicarOpcoesParaCategoria = BaseEndPoint.BaseUrl + "/OpcaoProduto/replicar/{0}/{1}";


        public const string GetValoresByOpcao = BaseEndPoint.BaseUrl + "/ValorOpcaoProduto/{0}/{1}/{2}/valores";
        public const string CreateValorOpcao = BaseEndPoint.BaseUrl + "/ValorOpcaoProduto/{0}/{1}/{2}/valores";
        public const string UpdateValorOpcao = BaseEndPoint.BaseUrl + "/ValorOpcaoProduto/{0}/{1}/{2}/valores/{3}";
        public const string DeleteValorOpcao = BaseEndPoint.BaseUrl + "/ValorOpcaoProduto/{0}/{1}/{2}/valores/{3}";

        public const string GetHorarioFuncionamentoByEstabelecimento = BaseEndPoint.BaseUrl + "/HorarioFuncionamento/estabelecimento/{0}";
        public const string IsEstabelecimentoAberto = BaseEndPoint.BaseUrl + "/HorarioFuncionamento/esta-aberto";
        public const string CreateHorarioFuncionamento = BaseEndPoint.BaseUrl + "/HorarioFuncionamento";
        public const string UpdateHorarioFuncionamento = BaseEndPoint.BaseUrl + "/HorarioFuncionamento/{0}";
        public const string DeleteHorarioFuncionamento = BaseEndPoint.BaseUrl + "/HorarioFuncionamento/{0}";
    }
}
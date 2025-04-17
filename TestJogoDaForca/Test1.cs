using Helper;
namespace TestJogoDaForca
{
    [TestClass]
    public class TestMenuHelper
    {
        [TestMethod]
        public void TestEscolhaSemCaracter()
        {
            //cenario
            string resultado = "";

            //acao
            string opcao = MenuHelper.ProcessarOpcao(resultado);

            //verificacao
            Assert.AreEqual("ErroVazio", opcao);
        }

        [TestMethod]
        public void TestEscolhaCaracterInvalida()
        {
            //cenario
            string resultado = "a";

            //acao
            string opcao = MenuHelper.ProcessarOpcao(resultado);

            //verificacao
            Assert.AreEqual("ErroInvalido", opcao);
        }
    }
  
}
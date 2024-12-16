namespace POORevision
{
  class Program
  {
    static void Main(string[] args)
    {
      var paymentBoleto = new PaymentBoleto();
      paymentBoleto.Pay();
      paymentBoleto.DateExpiration = DateTime.Now;
      paymentBoleto.NumberBoleto = "9829548";

      var paymentCard = new PaymentCreditCard();
      paymentCard.Pay();
      paymentCard.DateExpiration = DateTime.Now;
      paymentCard.NumberCard = "9829548";

      var payment = new Payment();
      payment.DateExpiration = DateTime.Now;
      payment.ToString();

      Console.WriteLine("Hello World!");
    }
  }

  //Encapsulamento
  // class Payment
  // {
  //   //Propriedades
  //   DateTime DatePayment;

  //   //Métodos
  //   void Pay()
  //   {
  //     ConsultPayment("9829548");
  //     Console.WriteLine("Payment");
  //   }

  //   private void ConsultPayment(string number)
  //   {
  //     Console.WriteLine("Consult Payment");
  //   }
  // }

  //Herança
  class Payment
  {
    public DateTime DateExpiration;

    public virtual void Pay() //Virtual permite que a classe filha sob
    {
      Console.WriteLine("Payment");
    }

    public override string ToString()
    {
      return DateExpiration.ToString("dd/MM/yyyy"); //Sobrescrevendo o método ToString
    }
  }
  class PaymentBoleto : Payment
  {
    public string? NumberBoleto;

    public override void Pay() //Override sobrescreve o método da classe pai
    {
      Console.WriteLine("Payment Boleto");
    }
  }

  class PaymentCreditCard : Payment
  {
    public string? NumberCard;

    public override void Pay()
    {
      Console.WriteLine("Payment Credit Card");
    }
  }

  // private, protected, internal and public
  public class ModificadoresDeAcesso //internal fica disponivel dentro do mesmo namespace
  {
    public string valorPublico = string.Empty; //aberto a todos
    private string? valorPrivado; //privado somente a classe
    protected int valorProtected; //aberto para classes filhas que herdam a classe pai
  }

  public class Acesso : ModificadoresDeAcesso
  {
    void Teste()
    {
      base.valorProtected = 1;
    }
  }
}
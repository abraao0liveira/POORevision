using System.Diagnostics;

namespace POORevision;
  class Program
  {
    //Delegates
    static void RealizarTest(double valor) //uma function que aponta para outra function
    {
      Console.WriteLine($"valor pago: {valor}.");
    }
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

      //using and dispose
      var construtor = new Construtor();
      construtor.Dispose(); //dispose é chamado para liberar recursos não gerenciados
      using (var construtor2 = new Construtor())
      {
        Console.WriteLine("Using");
      } //dispose é chamado automaticamente

      //Classe estática
      Setttings.API_URL = "http://localhost:5050";
      
      //Instanciando Classe Parcial
      var payments = new Payments();
      payments.PropriedadeOne = 1; //de Payments
      payments.PropriedadeTwo = 2; //de CreditCardPayment
      
      //Upcast and Downcast
      var pessoa = new Pessoa(1,"");
      pessoa = new PessoaFisica(1,""); //da classe filha para a classe pai (up)
      
      var person = new Pessoa(1,"");
      var pessoaJuridica = new PessoaJuridica(1,"");
      pessoaJuridica = (PessoaJuridica)person; //da classe pai para a classe filha (down)
      
      //Comparando objetos
      var pessoaA = new Pessoa(2,"TESTE");
      var pessoaB = new Pessoa(2,"TESTE");
      
      Console.WriteLine(pessoaA == pessoaB); //false, pois retornam os endereços
      Console.WriteLine(pessoaA.Equals(pessoaB)); //true
      
      //Delegates
      var testDelegate = new TestDelegate.Test(RealizarTest); //estou delegando a function
      testDelegate(25);
      
      //Events
      var room = new Room(2); //criando uma sala com 2 seats
      room.RoomSoldOutEvent += OnRoomSoldOut; //estou delegando o método ao evento
      room.ReserveSeat(); //reservando um seat
      room.ReserveSeat();
      room.ReserveSeat();
      
      //Tipos genericos
      var personOne = new Person();
      var context = new DataContext<IPerson, Subscription>();
      context.Save(personOne); //Espera o Tipo generico
      
      //Listas
      IEnumerable<TestList> testListOne = new List<TestList>(); //lista de leitura
      var testLists = new List<TestList>(); //lista com todos os métodos, Add, Remove
      testLists.Add(new TestList(1)); // add um dado a lista
      testLists.Add(new TestList(2));
      testLists.Remove(new TestList(1)); // remove um dado a lista

      foreach (var testList in testLists)
      {
        Console.WriteLine(testList.Id);
      }

      var listOne = testLists.Where(x => x.Id == 1); //busca todos os dados do "id" = 1 na lista
      var listTwo = testLists.First(x => x.Id == 1); //busca o primeiro dado do "id" = 1 na lista
      
      var multipleLists = new List<TestList>();
      multipleLists.AddRange(testLists); //Espera um IEnumerable, add uma lista dentro de outra
      
      //multipleLists.RemoveRange(); //remove lista dentro de lista
      multipleLists.Clear(); //limpa a lista
      
      //percorer todos os dados
      foreach (var item in testLists.Where(x => x.Id == 2))
      {
        Console.WriteLine(item.Id);
      }
      
      var exists = testLists.Any(x => x.Id == 3); //retorna true se o dado existir, ou false se não
      Console.WriteLine(exists);
      
      testLists.RemoveAll(x => x.Id == 3); //remove todos os dados com esse "id"

      var count = testLists.Count(x => x.Id == 3); //conta quantos itens tem na lista

      var enumerable = testLists.Skip(1); //pula um ‘item’ da lista
      
      var take1 = testLists.Take(2); //pega 2 itens da lista
      var take2 = testLists.Skip(1).Take(3);
      
      //conversão de listas
      var asEnumerable = testLists.AsEnumerable(); //convertendo List para IEnumerable
      var lists = asEnumerable.ToList(); //convertendo de IEnumerable para List
    }

    static void OnRoomSoldOut(object? sender, EventArgs e) // método que vai ser delegado
    {
      Console.WriteLine("Room Sold Out");
    }
  }

  //Listas
  public class TestList
  {
    public int Id { get; set; }

    public TestList(int id)
    {
      Id = id;
    }
  }
  
  //Events
  public class Room
  {
    public Room(int seats)
    {
      Seats = seats;
      _seatsInUse = 0;
    }

    private int _seatsInUse = 0;
    public int Seats { get; set; }

    public void ReserveSeat()
    {
      _seatsInUse++;
      if (_seatsInUse >= Seats)
      {
        OnRoomSoldOut(EventArgs.Empty);
      }
      else
      {
        Console.WriteLine($"Reserving seat {_seatsInUse}");
      }
    }

    public event EventHandler? RoomSoldOutEvent; //evento criado, definição, sempre com Event ao final

    protected virtual void
      OnRoomSoldOut(EventArgs e) //método manipulador, gerencia o evento, sempre começa com 'On', sempre void.
    {
      EventHandler? handler = RoomSoldOutEvent;
      handler?.Invoke(this, e);
    }
  }

  //Tipos Genericos
  public class DataContext<TP, TS> //T de tipo, padrão
    where TP : IPerson // obriga o tipo generico ser da classe Person
    where TS : Subscription
  {
    public void Save(TP entity)
    {
    }
    public void Save(TS entity)
    {
    }
  }

  public interface IPerson
  {
  }
  public class Person : IPerson
  {
  }
  public class Subscription
  {
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
      base.valorProtected = 1; //atribuindo um valor a uma variavel protected da classe pai
    }
  }

  //Tipos complexos
  public class Pagamento
  {
    Adress? BillingAddres; //variavel de tipo complexo
    public DateTime Vencimento { get; set; } //Propriedade (prop)
    private DateTime _dataPagamento; //(propFull) - variavel privada comeca com "_"
    public DateTime DataPagamento //(propFull)
    {
      //possiblidade de add logicas aos metodos get e set, como um Console.WriteLine(), por exemplo
      get
      {
        Console.WriteLine("Lendo um valor."); //add
        return _dataPagamento.AddDays(1);
      }
      set { _dataPagamento = value; }
    }
    public int Propg { get; private set; }  //(propg) Aberta apenas para leitura, mas sem a possibilidade de setar um valor a ela
  }
  public class Adress
  {
    string ZipCode = string.Empty;
  }

  //Metodos
  public class Metodos
  {
    public DateTime DataPagamento { get; set; }

    //metodo construtor

    //public Metodos() { } 
    //construtor paramiteriz, sem parametros, para o filho poder acessar o construtor sem parametros obrigatórios
    public Metodos(DateTime vencimento) //ctor
    {
      Console.WriteLine("Construtor");
      DataPagamento = DateTime.Now;
    }

    //Sobrecarga de métodos
    public void Pagar(string numero) { }
    public void Pagar(string numero, DateTime vencimento) { }
    public void Pagar(string numero, DateTime vencimento, bool pagarPosVencimento = false) { }

    //Sobreescrever métodos
    public virtual void PagarSob(string text)
    {
      Console.WriteLine("Pagar");
    } //metodo com a possibilidade de ser sobrescrito
  }

  public class MetodoSob : Metodos
  {
    //metodo construtor com parametros obrigatorios do pai
    public MetodoSob(DateTime vencimento) : base(vencimento)
    {
    }
    public override void PagarSob(string text)
    {
      Console.WriteLine("Pagar Sobrescrito");
    } //metodo sobrescrito
  }

  //Using and Dispose
  public class Construtor : IDisposable
  {
    public Construtor()
    {
      var construtor = new Construtor();
      Console.WriteLine("Construtor");
    }

    public void Dispose()
    {
      Console.WriteLine("Finalizador");
    }
  }

  //Classe estática
  public static class Setttings
  {
    public static string API_URL { get; set; } = string.Empty;
  }
  
  //Classe Selada 
  public sealed class ClasseSelada //classe que não pode ser herdada por outra
  {
    public string Teste { get; set; } = string.Empty;
  }
  
  //A Interface, contrato da classe 
  public abstract class TestPayment : IPayment //classe deve seguir tudo que a 'interface' pede.
  { //Uma classe 'abstract' não pode ser instanciada
    public DateTime DateExpiration { get; set; }

    public virtual void Pay(double value)
    {
    }
  }

  public class TestPaymentCreditCard : TestPayment
  {
    public override void Pay(double value)
    {
      base.Pay(value);
    }
  }

  public class TestPaymentBoleto : TestPayment
  {
    public override void Pay(double value)
    {
      base.Pay(value);
    }
  }

  public interface IPayment
  {
    //posso criar a minha propriedade, métodos e eventos
    DateTime DateExpiration { get; set; }
    
    void Pay(double value);
  }
//'Interface' é um contrato que define o que tem que ser feito, e a 'abstract' tem uma implementação base do que deve ser feito.

  //Up and Down
  public class Pessoa : IEquatable<Pessoa>
  {
    public Pessoa(int id, string nome)
    {
      Id = id;
      Nome = nome;
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Equals(Pessoa? pessoa)
    {
      return Id == pessoa!.Id;
    }
  }
  public class PessoaFisica : Pessoa
  {
    public PessoaFisica(int id, string nome) : base(id, nome)
    {
    }

    public string CPF { get; set; } = string.Empty;
    
  }
  public class PessoaJuridica : Pessoa
  {
    public PessoaJuridica(int id, string nome) : base(id, nome)
    {
    }

    public string CNPJ { get; set; } = string.Empty;
  }
  
  //Delegate
  public class TestDelegate
  {
    public delegate void Test(double valor); //Quem for de fora pode fazer essa função.
  }
namespace SmartHouseApp.Patterns.Behavioral.Visitor
{
     public interface IVisitable
     {
          void Accept(IVisitor visitor);
     }
}
namespace SmartHouseApp.Patterns.Behavioral.Visitor
{
     public class LightDevice : IVisitable
     {
          public void Accept(IVisitor visitor)
          {
               visitor.Visit(this);
          }
     }
}
using System;
public class AppFacade:Facade{
  
  protected AppFacade():base(){
         
  }

  private static AppFacade m_instance;
  public static AppFacade Instance{
      get{
          if(m_instance == null){
              m_instance = new AppFacade();
          }
          return m_instance;
      }
  }

  public override void InitFramework(){

      base.InitFramework();
      RegisterCommand(Notice.START_UP,typeof(StartUpCommand));
  }

  public void StartUp(){
      ExecuteCommand(Notice.START_UP);
      RemoveCommand(Notice.START_UP);
  }

}
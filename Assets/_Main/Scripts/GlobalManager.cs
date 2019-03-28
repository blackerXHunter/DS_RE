public class GlobalManager : Singleton<GlobalManager>
{
    public void Start(){
        globalInput = UserInput.GetEnabledUserInput(gameObject);
    }
    public UserInput globalInput;
}
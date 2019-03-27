class BoxAMHandler : IActorManagerHandler
{
    public override void DoAction()
    {

    }

    public override void Handle(string command, object[] objs)
    {
        if (command == "Lock")
        {
            Lock((bool)objs[0]);
        }
    }

    private void Lock(bool val)
    {
        am.ac.IssueBool("lock", val);
    }

}
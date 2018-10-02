public class Selector : Composite
{
    protected override bool MustAllChildrenSucceed
    {
        get
        {
            return false;
        }
    }

    public override bool Execute()
    {
        bool result = CheckCondition();

        if (result)
        {
            foreach (Node node in children)
            {
                result = result || node.Execute();

                if (ShouldBreak(result))
                {
                    break;
                }
            }
            
        }

        return result;
    }

    protected override bool ShouldBreak(bool result)
    {
        return result;
    }

    protected virtual bool CheckCondition()
    {
        return true;
    }
}
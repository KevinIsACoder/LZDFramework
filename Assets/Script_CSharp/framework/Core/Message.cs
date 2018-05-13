using System;
public class Message:IMessage{

    private string m_name;
    private string m_type;
    private object m_body;
    public Message(string msgName){
        m_name = msgName;
        m_body = null;
        m_type = null;
    }

    public Message(string msgName,object body){
        m_name = msgName;
        m_body = body;
        m_type = null;
    }
    public virtual string Name{

        get{
             return m_name;
        }
        set{
            
        }
    }

    public virtual Object Body{
        get{

            return m_body;

        }

        set{

        }
    }

    public virtual string CommandType{

        get{
            return m_type;
        }
        set{

        }
    }
}
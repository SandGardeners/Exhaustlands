using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CommandsManager : MonoBehaviour
{        List<Command> m_Commands = new List<Command>();
        List<Command> m_CommandsHistory = new List<Command>();
        public bool canBeShortcuted
        {
            get
            {
                return m_CommandsHistory.Count == 0 || m_CommandsHistory[m_CommandsHistory.Count-1] is InterruptableCommand;
            }
        }
        public void AddCommand(Command theCommand)
        {
            m_Commands.Add(theCommand);
            Debug.Log("Added a command " + m_Commands.Count);
        }

        CoroutineQueue queue;

        public void ExecuteCommand()
        {
            if(queue == null)
            {
                queue = new CoroutineQueue(1, StartCoroutine);
                queue.finished += ClearCommands;
            }
            
            foreach (Command theCommand in m_Commands)
            {
                m_CommandsHistory.Add(theCommand);
               queue.Run(theCommand.Execute());
            }
            m_Commands.Clear();
        }

    public void ClearCommands()
    {
        if(queue != null)
        {
            queue.ClearQueue();
            foreach(Command command in m_CommandsHistory)
            {
                if(!command.isCompleted)
                {
                    StopCoroutine(command.Execute());
                    if(command is InterruptableCommand)
                        ((InterruptableCommand)command).Interrupt();
                    command.Complete();
                }
            }
            m_CommandsHistory.Clear();
        }
    }
}
    /// <summary>
    /// 命令抽象类
    /// </summary>
    public abstract class Command
    {
        protected static int count = 0;
		public bool isCompleted { get; protected set; }
        protected int id;
        public abstract IEnumerator Execute();
		public void Complete()
		{
			isCompleted = true;
		}
    }


    /// <summary>
    /// 实际命令1-绑定命令和receiver
    /// </summary>
    public class ConcreteCommand : Command
    {
        protected ICommandReceiver m_Receiver = null;
        protected object[] m_params;
        public ConcreteCommand(ICommandReceiver Receiver, params object[] _params)
        {
            id = Command.count++;
            m_params = _params;
            m_Receiver = Receiver;
        }

        public override IEnumerator Execute()
        {
            m_Receiver.ExecuteCommand(this, m_params);
			while(!isCompleted)
			{
                //Debug.Log("COROUTINE #"+ id + " GOING ON " + m_Receiver);
				yield return null;
			}
        }
    }

    public class CameraTransitionCommand : ConcreteCommand
    {
        public CameraTransitionCommand(ICommandReceiver Receiver, params object[] _params) : base(Receiver, _params)
        {
            m_Receiver = new GameObject().AddComponent<CameraTransitionReceiver>();
        }
    }

    public class InterruptableCommand : ConcreteCommand
    {
        public InterruptableCommand(IInterruptableCommandReceiver Receiver, params object[] _params) : base(Receiver, _params)
        {
            
        }

        public void Interrupt()
        {
            ((IInterruptableCommandReceiver)(m_Receiver)).Interrupt(this);
        }
    }

public class DisplayCommand : ConcreteCommand
{
    public DisplayCommand(ICommandReceiver Receiver, string displayType, string dataID) : base(Receiver, displayType, dataID)
    {

    }
}

public interface ICommandReceiver
	{
		void ExecuteCommand(Command _command, params object[] _params);
	}

    public interface IInterruptableCommandReceiver : ICommandReceiver
    {
        void Interrupt(InterruptableCommand _command);
    }

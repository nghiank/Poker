using System;

public interface ICommand
{
	void perform(EventDispatcher dispatcher);
}


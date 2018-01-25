using System;

public class UserInfo
{
	private string displayName;

	public UserInfo(UserInfoBuilder builder)
	{
		this.displayName = builder.getDisplayName();
	}

	public class UserInfoBuilder 
	{
		private string displayName;
		public UserInfoBuilder DisplayName(string displayName) 
		{
			this.displayName = displayName;
			return this;
		}

		public string getDisplayName() {
			return this.displayName;
		}
	}
}


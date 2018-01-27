using System;

public class UserInfo
{
	private string displayName;
	private string userId;

	public UserInfo(UserInfoBuilder builder)
	{
		this.displayName = builder.getDisplayName();
		this.userId = builder.getUserId ();
	}
		
	public string getDisplayName() {
		return displayName;
	}

	public string getUserId() {
		return userId;
	}

	public class UserInfoBuilder 
	{
		private string displayName;
		private string userId;
		public UserInfoBuilder DisplayName(string displayName) 
		{
			this.displayName = displayName;
			return this;
		}

		public UserInfoBuilder UserId(string userId) {
			this.userId = userId;
			return this;
		}

		public string getDisplayName() {
			return this.displayName;
		}

		public string getUserId() {
			return this.userId;
		}
	}
}


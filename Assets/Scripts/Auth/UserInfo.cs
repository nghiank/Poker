using System;

public class UserInfo
{
	private string displayName;
	private string userId;

	public UserInfo(UserInfoBuilder builder)
	{
		this.displayName = builder.GetDisplayName();
		this.userId = builder.GetUserId ();
	}
		
	public string GetDisplayName() {
		return displayName;
	}

	public string GetUserId() {
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

		public string GetDisplayName() {
			return this.displayName;
		}

		public string GetUserId() {
			return this.userId;
		}
	}
}


namespace InternetBank.Api.Requests.Accounts;

public record ChangeAccountPasswordRequest(string OldPassword, string NewPassword, string RepeatNewPassword);
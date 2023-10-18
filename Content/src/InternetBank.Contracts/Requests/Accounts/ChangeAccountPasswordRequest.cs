namespace InternetBank.Contracts.Requests.Accounts;

public record ChangeAccountPasswordRequest(string OldPassword, string NewPassword, string RepeatNewPassword);
namespace InternetBank.Contracts.Requests.Accounts;

public record ChangeAccountPasswordRequest(string AccountId,
                                           string OldPassword,
                                           string NewPassword,
                                           string RepeatNewPassword);
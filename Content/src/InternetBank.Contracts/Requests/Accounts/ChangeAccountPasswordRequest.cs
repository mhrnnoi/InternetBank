namespace InternetBank.Contracts.Requests.Accounts;

public record ChangeAccountPasswordRequest(int AccountId,
                                           string UserId,
                                           string OldPassword,
                                           string NewPassword,
                                           string RepeatNewPassword);
﻿namespace Interface;
public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}

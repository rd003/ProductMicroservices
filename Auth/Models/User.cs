namespace Auth.Models;

public record User(string Username, string Password, string Role, string[] Scopes);

package com.example.server.DTO.User;

public class SignInDto {
    private String email;
    private String password;
    public SignInDto(String email, String password) {
        this.email = email;
        this.password = password;
    }
    public SignInDto() {
    }
    public String getEmail() {
        return email;
    }
    public void setEmail(String email) {
        this.email = email;
    }
    public String getPassword() {
        return password;
    }
    public void setPassword(String password) {
        this.password = password;
    }   

}

package com.example.server.DTO.User;

import com.example.server.Enums.Gender;

public class SignUpDto {
    private String userName;
    private String email;
    private String password;
    private String avatar;
    private Gender gender;

    public SignUpDto(){

    }

    
    public SignUpDto(String userName, String email, String password, String avatar, Gender gender) {
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.avatar = avatar;
        this.gender = gender;
    }
    public String getUserName() {
        return userName;
    }
    public void setUserName(String userName) {
        this.userName = userName;
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
    public String getAvatar() {
        return avatar;
    }
    public void setAvatar(String avatar) {
        this.avatar = avatar;
    }
    public Gender getGender() {
        return gender;
    }
    public void setGender(Gender gender) {
        this.gender = gender;
    }

    

}

package com.example.server.DTO.User;

import com.example.server.Enums.Gender;
import com.example.server.Enums.Role;

public class UserCreatedDto {
    private String userName;
    private String email;
    private String password;
    private String avatar;
    private Gender gender;
    private Role role;

    public Role getRole() {
        return role;
    }


    public void setRole(Role role) {
        this.role = role;
    }


    public UserCreatedDto(){

    }

    
    public UserCreatedDto(String userName, String email, String password, String avatar, Gender gender, Role role) {
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.avatar = avatar;
        this.gender = gender;
        this.role = role;
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

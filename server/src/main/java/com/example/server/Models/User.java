package com.example.server.Models;

import java.util.Date;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import com.example.server.Enums.Gender;
import com.example.server.Enums.Role;


@Document(collection = "users")
public class User{
    
    @Id
    private String id;
    private String userName;
    private String email;
    private String password;
    private String avatar;
    private Gender gender;
    private Role role;

    private Date createdAt;
    private Date updatedAt;

    public User(){
        this.createdAt = new Date();
        this.updatedAt = new Date();
    }

    public User(String userName, String email, String password, String avatar, Gender gender, Role role) {
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.avatar = avatar;
        this.gender = gender;
        this.role = role;
    }


    public String getId() {
        return id;
    }
    public void setId(String id) {
        this.id = id;
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

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }
    public Date getCreatedAt() {
        return createdAt;
    }
    public void setCreatedAt(Date createdAt) {
        this.createdAt = createdAt;
    }
    public Date getUpdatedAt() {
        return updatedAt;
    }
    public void setUpdatedAt(Date updatedAt) {
        this.updatedAt = updatedAt;
    }

    @Override
    public String toString() {
        return "User [id=" + id + ", userName=" + userName + ", email=" + email + ", password=" + password + ", avatar="
                + avatar + ", gender=" + gender + ", createdAt=" + createdAt + ", updatedAt=" + updatedAt + "]";
    }

}

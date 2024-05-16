package com.example.server.Models;
import java.util.Date;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "wallet")
public class Wallet {
    @Id
    private String id;

    private String userId;

    private Double amount;

    private Date transactionRecent;

    private Date createdAt;

    private Date updatedAt;

    public Wallet(){
        this.createdAt = new Date();
        this.updatedAt = new Date();
    }

    public Wallet(String id, String userId, Double amount, Date transactionRecent) {
        this.id = id;
        this.userId = userId;
        this.amount = amount;
        this.transactionRecent = transactionRecent;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public Double getAmount() {
        return amount;
    }

    public void setAmount(Double amount) {
        this.amount = amount;
    }

    public Date getTransactionRecent() {
        return transactionRecent;
    }

    public void setTransactionRecent(Date transactionRecent) {
        this.transactionRecent = transactionRecent;
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
        return "Wallet [id=" + id + ", userId=" + userId + ", amount=" + amount + ", transactionRecent="
                + transactionRecent + ", createdAt=" + createdAt + ", updatedAt=" + updatedAt + "]";
    }
}

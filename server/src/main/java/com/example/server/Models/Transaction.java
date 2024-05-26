package com.example.server.Models;

import java.util.Date;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import com.example.server.Enums.TransactionType;

@Document(collection = "transactions")
public class Transaction {
    
    @Id
    private String id;

    private String userId;
    private String budgetId;
    private String categoryId;
    private Double amount;

    private TransactionType transactionType; // loại giao dịch: thu nhập hoặc chi tiêu
    private String description;
    private Date createdAt;
    private Date updatedAt;

    public Transaction(){
        this.createdAt = new Date();
        this.updatedAt = new Date();
    }
    
    public Transaction(String userId, String budgetId, String categoryId, Double amount,
            TransactionType transactionType, String description) {
        this.userId = userId;
        this.budgetId = budgetId;
        this.categoryId = categoryId;
        this.amount = amount;
        this.transactionType = transactionType;
        this.description = description;
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
    public String getBudgetId() {
        return budgetId;
    }
    public void setBudgetId(String budgetId) {
        this.budgetId = budgetId;
    }
    public String getCategoryId() {
        return categoryId;
    }
    public void setCategoryId(String categoryId) {
        this.categoryId = categoryId;
    }
    public Double getAmount() {
        return amount;
    }
    public void setAmount(Double amount) {
        this.amount = amount;
    }
    public TransactionType getTransactionType() {
        return transactionType;
    }
    public void setTransactionType(TransactionType transactionType) {
        this.transactionType = transactionType;
    }
    public String getDescription() {
        return description;
    }
    public void setDescription(String description) {
        this.description = description;
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
        return "Transaction [id=" + id + ", userId=" + userId + ", budgetId=" + budgetId + ", categoryId=" + categoryId
                + ", amount=" + amount + ", transactionType=" + transactionType + ", description=" + description
                + ", createdAt=" + createdAt + ", updatedAt=" + updatedAt + "]";
    }



}

package com.example.server.Models;
import java.util.Date;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import com.example.server.Enums.BudgetStatus;

@Document(collection = "budgets")
public class Budget {

    @Id
    private String id;
    private String userId;
    private String budgetName;
    private Double amount;
    private Date startDate;
    private Date endDate;

    private BudgetStatus status;

    private Date createdAt;
    private Date updatedAt;

    public Budget(){
        this.createdAt = new Date();
        this.updatedAt = new Date();
    }

    
    public Budget(String userId, String budgetName, Double amount, Date startDate, Date endDate, BudgetStatus status, Date createdAt,
            Date updatedAt) {
        this.userId = userId;
        this.budgetName = budgetName;
        this.amount = amount;
        this.startDate = startDate;
        this.endDate = endDate;
        this.status = status;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }
    public String getId() {
        return id;
    }
    public void setId(String id) {
        this.id = id;
    }
    public String getBudgetName() {
        return budgetName;
    }
    public void setBudgetName(String budgetName) {
        this.budgetName = budgetName;
    }
    public Double getAmount() {
        return amount;
    }
    public void setAmount(Double amount) {
        this.amount = amount;
    }
    public Date getStartDate() {
        return startDate;
    }
    public void setStartDate(Date startDate) {
        this.startDate = startDate;
    }
    public Date getEndDate() {
        return endDate;
    }
    public void setEndDate(Date endDate) {
        this.endDate = endDate;
    }
    public BudgetStatus getStatus() {
        return status;
    }
    public void setStatus(BudgetStatus status) {
        this.status = status;
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

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }


    @Override
    public String toString() {
        return "Budget [id=" + id + ", userId=" + userId + ", budgetName=" + budgetName + ", amount=" + amount
                + ", startDate=" + startDate + ", endDate=" + endDate + ", status=" + status + ", createdAt="
                + createdAt + ", updatedAt=" + updatedAt + "]";
    }


   

    
}

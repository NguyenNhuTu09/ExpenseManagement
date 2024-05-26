package com.example.server.Service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.server.Exceptions.CustomException;
import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Budget;
import com.example.server.Repository.BudgetRepository;
import com.example.server.Repository.UserRepository;

@Service
public class BudgetService {
    
    @Autowired
    BudgetRepository budgetRepository;

    @Autowired
    UserRepository userRepository;

    public Budget addBudget(Budget budget) throws CustomException {
        if (budget.getUserId() == null || budget.getAmount() <= 0) {
            throw new CustomException("Invalid budget data");
        }
        return budgetRepository.save(budget);
    }

    public Budget updateBudget(Budget budget) throws DataNotExistException {
        Budget existingBudget = budgetRepository.findBudgetById(budget.getId());
        if (existingBudget == null) {
            throw new DataNotExistException("Budget is not present");
        }
        existingBudget.setAmount(budget.getAmount());
        existingBudget.setStartDate(budget.getStartDate());
        existingBudget.setEndDate(budget.getEndDate());
        return budgetRepository.save(existingBudget);
    }

    public void deleteBudget(String budgetId) throws DataNotExistException {
        Budget budget = budgetRepository.findBudgetById(budgetId);
        if (budget == null) {
            throw new DataNotExistException("Budget is not present");
        }
        budgetRepository.delete(budget);
    }

    public Budget getSingleBudget(String budgetId) throws DataNotExistException {
        Budget budget = budgetRepository.findBudgetById(budgetId);
        if (budget == null) {
            throw new DataNotExistException("Budget is not present");
        }
        return budget;
    }

    public List<Budget> getBudgetsByUser(String userId) {
        return budgetRepository.findByUserId(userId);
    }

    public List<Budget> getBudgetsByUserAndCategory(String userId, String category) {
        return budgetRepository.findByUserIdAndCategory(userId, category);
    }
}

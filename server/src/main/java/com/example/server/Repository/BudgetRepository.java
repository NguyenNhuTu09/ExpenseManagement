package com.example.server.Repository;

import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.Budget;

@Repository
public interface  BudgetRepository extends MongoRepository<Budget, String> {
    List<Budget> findByUserId(String userId);
    List<Budget> findByUserIdAndCategory(String userId, String category);
    Budget findBudgetById(String id);
}

package com.example.server.Repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.Budget;

@Repository
public interface  BudgetRepository extends MongoRepository<Budget, String> {
    
}

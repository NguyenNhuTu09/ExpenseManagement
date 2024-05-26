package com.example.server.Repository;

import java.util.List;

import org.springframework.data.mongodb.repository.MongoRepository;
import  org.springframework.stereotype.Repository;

import com.example.server.Models.Category;

@Repository
public interface CategoryRepository extends MongoRepository<Category, Object> {
    List<Category> findByUserId(String userId);
    Category findCategoryById(String id);
}   

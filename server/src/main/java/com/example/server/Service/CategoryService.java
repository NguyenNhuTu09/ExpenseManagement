package com.example.server.Service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.server.Exceptions.CustomException;
import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Category;
import com.example.server.Repository.CategoryRepository;
import com.example.server.Repository.UserRepository;

@Service
public class CategoryService {
    
    @Autowired
    CategoryRepository categoryRepository;

    @Autowired
    UserRepository userRepository;

    public Category addCategory(Category category) throws CustomException {
        if (category.getCategoryName() == null) {
            throw new CustomException("Invalid category data");
        }
        return categoryRepository.save(category);
    }

    public Category updateCategory(Category category) throws DataNotExistException {
        Category existingCategory = categoryRepository.findCategoryById(category.getId());
        if (existingCategory == null) {
            throw new DataNotExistException("Category is not present");
        }
        existingCategory.setDescription(category.getDescription());
        return categoryRepository.save(existingCategory);
    }

    public void deleteCategory(String categoryId) throws DataNotExistException {
        Category category = categoryRepository.findCategoryById(categoryId);
        if (category == null) {
            throw new DataNotExistException("Category is not present");
        }
        categoryRepository.delete(category);
    }

    public Category getSingleCategory(String categoryId) throws DataNotExistException {
        Category category = categoryRepository.findCategoryById(categoryId);
        if (category == null) {
            throw new DataNotExistException("Category is not present");
        }
        return category;
    }

    public List<Category> getCategoriesByUser(String userId) {
        return categoryRepository.findByUserId(userId);
    }

}

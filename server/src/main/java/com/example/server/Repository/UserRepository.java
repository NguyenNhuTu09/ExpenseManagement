package com.example.server.Repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.User;

@Repository
public interface  UserRepository extends MongoRepository<User, String> {
    
    // List<User> findAll();

    User findByEmail(String theId);

    User findUserById(String theId);
    
    User findUserByEmail(String email);
}

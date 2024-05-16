package com.example.server.Repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.AuthenticationToken;
import com.example.server.Models.User;

@Repository
public interface TokenRepository extends MongoRepository<AuthenticationToken, Object> {
    AuthenticationToken findTokenByUser(User user);
    AuthenticationToken findTokenByToken(String token);
}

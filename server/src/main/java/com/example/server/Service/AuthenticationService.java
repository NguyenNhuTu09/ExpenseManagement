package com.example.server.Service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.server.Config.MessageStrings;
import com.example.server.Exceptions.AuthenticationFailException;
import com.example.server.Models.AuthenticationToken;
import com.example.server.Models.User;
import com.example.server.Repository.TokenRepository;
import com.example.server.Util.Helper;

@Service
public class AuthenticationService {
    
    @Autowired
    TokenRepository repository;

    public void saveConfirmationToken(AuthenticationToken authenticationToken){
        repository.save(authenticationToken);
    }

    public AuthenticationToken getToken(User user){
        return repository.findTokenByUser(user);
    }

    public User getUser(String token){
        AuthenticationToken authenticationToken = repository.findTokenByToken(token);
        if(Helper.notNull(authenticationToken)){ 
            if(Helper.notNull(authenticationToken.getUser())){ 
                return authenticationToken.getUser();
            }
        }
        return null;
    }

    public void authenticate(String token) throws AuthenticationFailException{
        if(!Helper.notNull(token)){
            throw new AuthenticationFailException(MessageStrings.AUTH_TOEKN_NOT_PRESENT); 
        }
        if(!Helper.notNull(getUser(token))){
            throw new AuthenticationFailException(MessageStrings.AUTH_TOEKN_NOT_VALID); 
        }
    }
}

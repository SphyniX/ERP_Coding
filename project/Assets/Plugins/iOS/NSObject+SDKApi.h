//
//  NSObject+SDKApi.h
//  Unity-iPhone
//
//  Created by xing weizhen on 11/10/14.
//
//

#import <Foundation/Foundation.h>

@interface SDKApi : NSObject

@property (nonatomic)   NSString* method;
@property (nonatomic)   NSDictionary* param;
@property (nonatomic)   NSString* name;
@property (nonatomic)   NSString* type;
@property (nonatomic) const char* charname;
//-----------------------------------------
-(void)setMethod:(NSDictionary *)dict;
-(void)setParam:(NSDictionary *)dict;

//-----------------------------------------
//-(void)OnGameMesssage:(NSString *)param;
-(NSString*)OnGameMesssageRet:(NSString *)param;
-(void)OnGameLaunch;
+(SDKApi *)Instance;
@end

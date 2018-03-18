value=[50 40 30 50 30 24 36 22 41 75];
weight=[15 4 6 3 2 6 7 1 9 5];
TotalWeight=20;
beta=0:.01:1;
n=1000;
dotsarr = zeros(1,250);

timeDeterArr = zeros(1,250);
valueDeterArr = zeros(1,250);

timerArr = zeros(1,250);
valueArr = zeros(1,250);

for idx=1:250
    [result, time] = Knapsack( value, weight, TotalWeight, beta, n);
    timerArr(idx) = time;
    valueArr(idx) = result;
    
    [deterResult, deterTime] = KnapsackDeter( value, weight, TotalWeight, beta, n);
    timeDeterArr(idx) = deterTime;
    valueDeterArr(idx) = deterResult;
    
    dotsarr(idx) = idx;
end

ax1 = subplot(2,1,1); % top subplot
plot(dotsarr, timeDeterArr, dotsarr, timerArr,'--');
title(ax1,'Временная шкала')
ylabel(ax1,'t, сек')

ax2 = subplot(2,1,2); % bottom subplot
plot(dotsarr, valueDeterArr, dotsarr, valueArr, '--');
title(ax2,'Шкала суммарной значимости')
ylabel(ax2,'Значимость, усл.ед.')
ylim([215 230])

function [outValue, outTime] = Knapsack( value, weight, TotalWeight, beta, n )
% Input: value = array of values associated with object i.
% weight = array of weights associated with object i.
% TotalWeight = the total weight one can carry in the knapsack.
% beta = vector of beta values for simulated annealing.
% n = number of simulations per beta value.
% Output: FinalValue = maximum value of objects in the knapsack.
% FinalItems = list of objects carried in the knapsack.
% Entries in the vector correspond to object i
% being present in the knapsack.
tic
v=length(value);
W=zeros(1,v);
Value=0;
VW=0;
a=length(beta);
nn=n*ones(1,a);
tStart = tic;
for i=1:a
    b=beta(i);
    for j=2:nn(i)
        m=0;
        while m==0
            c=ceil(rand*v);
            if W(c)==0
                m=1;
            end
        end
        TrialW=W;
        TrialW(c)=1;
        while sum(TrialW.*weight)>TotalWeight
            e=0;
            while e==0
                d=ceil(rand*v);
                if TrialW(d)==1
                    e=1;
                end
            end
            TrialW(d)=0;
        end
        f=sum(TrialW.*value)-sum(W.*value);
        g=min([1 exp(b*f)]);
        accept=(rand<=g);
        %Deterministic Model
        %if f>=0
        if accept
            W=TrialW;
            VW(j)=sum(W.*value);
        else
            VW(j)=VW(j-1);
        end
    end
    Value=[Value VW(2:length(VW))];
end
FinalValue=Value(length(Value))
x=0;
for k=1:length(W)
    if W(k)==1
        x=[x k];
    end
end
FinalItems=x(2:length(x))
tElapsed = toc(tStart);
outValue = FinalValue;
outTime = tElapsed;
end

function [outValue, outTime] = KnapsackDeter( value, weight, TotalWeight, beta, n )
% Input: value = array of values associated with object i.
% weight = array of weights associated with object i.
% TotalWeight = the total weight one can carry in the knapsack.
% beta = vector of beta values for simulated annealing.
% n = number of simulations per beta value.
% Output: FinalValue = maximum value of objects in the knapsack.
% FinalItems = list of objects carried in the knapsack.
% Entries in the vector correspond to object i
% being present in the knapsack.
tic
v=length(value);
W=zeros(1,v);
Value=0;
VW=0;
a=length(beta);
nn=n*ones(1,a);
tStart = tic;
for i=1:a
    b=beta(i);
    for j=2:nn(i)
        m=0;
        while m==0
            c=ceil(rand*v);
            if W(c)==0
                m=1;
            end
        end
        TrialW=W;
        TrialW(c)=1;
        while sum(TrialW.*weight)>TotalWeight
            e=0;
            while e==0
                d=ceil(rand*v);
                if TrialW(d)==1
                    e=1;
                end
            end
            TrialW(d)=0;
        end
        f=sum(TrialW.*value)-sum(W.*value);
        g=min([1 exp(b*f)]);
        accept=(rand<=g);
        %Deterministic Model
        if f>=0
        %if accept
            W=TrialW;
            VW(j)=sum(W.*value);
        else
            VW(j)=VW(j-1);
        end
    end
    Value=[Value VW(2:length(VW))];
end
FinalValue=Value(length(Value))
x=0;
for k=1:length(W)
    if W(k)==1
        x=[x k];
    end
end
FinalItems=x(2:length(x))
tElapsed = toc(tStart);
outValue = FinalValue;
outTime = tElapsed;
end